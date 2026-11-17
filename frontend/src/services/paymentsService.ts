const PAYMENTS_API_BASE = "/payments";

export interface CreateProductRequest {
	name: string;
	/** Amount in the smallest currency unit, e.g. 1099 = $10.99. */
	amount: number;
	currency: string;
}

export interface Product {
	id: string;
	object: string;
	name?: string | null;
	default_price?: unknown;
}

export interface CreatePriceRequest {
	currency: string;
	productId: string;
	unitAmount: number;
}

export interface Price {
	id: string;
	object: string;
	currency?: string | null;
	product?: string | null;
	unit_amount?: number | null;
}

export interface CreateCheckoutSessionRequest {
	priceId: string;
	quantity: number;
}

export interface CheckoutSession {
	id: string;
	object: string;
	/** The backend forwards Stripe's session verbatim, so fields are snake_case. */
	client_secret?: string;
	clientSecret?: string;
	amount_total?: number | null;
	currency?: string | null;
	status?: string | null;
	ui_mode?: string | null;
	url?: string | null;
}

export interface CheckoutOptions extends CreateProductRequest {
	quantity: number;
}

export const defaultCheckoutOptions: CheckoutOptions = {
	name: "T-shirt",
	amount: 1099,
	currency: "usd",
	quantity: 1,
};

async function postJson<TResponse>(
	path: string,
	body: unknown,
): Promise<TResponse> {
	let response: Response;

	try {
		response = await fetch(`${PAYMENTS_API_BASE}/${path}`, {
			method: "POST",
			headers: { "Content-Type": "application/json" },
			body: JSON.stringify(body),
		});
	} catch (cause) {
		throw new Error(`Could not reach the payments API (${path})`, { cause });
	}

	if (!response.ok) {
		const detail = await response.text().catch(() => "");
		throw new Error(
			`Payments API ${path} failed (${response.status})${detail ? `: ${detail}` : ""}`,
		);
	}

	return (await response.json()) as TResponse;
}

function createProduct(body: CreateProductRequest): Promise<Product> {
	return postJson<Product>("Product/Create", body);
}

function createPrice(body: CreatePriceRequest): Promise<Price> {
	return postJson<Price>("Price/Create", body);
}

function createCheckoutSession(
	body: CreateCheckoutSessionRequest,
): Promise<CheckoutSession> {
	return postJson<CheckoutSession>("CheckoutSession/Create", body);
}

async function createCheckoutFlow(
	options: CheckoutOptions = defaultCheckoutOptions,
): Promise<string> {
	const { name, amount, currency, quantity } = options;

	const product = await createProduct({ name, amount, currency });
	if (!product.id) {
		throw new Error("Product/Create response did not include a product id");
	}

	const price = await createPrice({
		currency,
		productId: product.id,
		unitAmount: amount,
	});
	if (!price.id) {
		throw new Error("Price/Create response did not include a price id");
	}

	const session = await createCheckoutSession({ priceId: price.id, quantity });
	const clientSecret = session.client_secret ?? session.clientSecret;
	if (!clientSecret) {
		throw new Error(
			"CheckoutSession/Create response did not include a client secret",
		);
	}

	return clientSecret;
}

// React StrictMode invokes effects twice in development. Share the in-flight
// request so that a double mount does not create duplicate Stripe resources.
let pendingCheckout: Promise<string> | null = null;

/**
 * The Checkout Session endpoint needs a Stripe price ID, so create the product
 * and price first using the endpoints exposed by the restructured API.
 *
 * Returns the session's client secret used to initialize Checkout Elements.
 */
function createCheckout(options?: CheckoutOptions): Promise<string> {
	pendingCheckout ??= createCheckoutFlow(options).finally(() => {
		pendingCheckout = null;
	});
	return pendingCheckout;
}

export const paymentsService = {
	createProduct,
	createPrice,
	createCheckoutSession,
	createCheckout,
};
