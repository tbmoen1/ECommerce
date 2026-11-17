import { CheckoutElementsProvider } from "@stripe/react-stripe-js/checkout";
import { loadStripe } from "@stripe/stripe-js";
import { useEffect, useState } from "react";
import { paymentsService } from "../services/paymentsService";
import CheckoutForm from "./Checkoutform";

const stripePromise = loadStripe(
	"pk_test_51UWISPE2gKP95laDsQa4j19Sb14fe44TeWtlqZOEwFJ3NdxAYySFPRqZL6iIqlJwb4ktAjOJanMIo4pnwWEES9CT00yDwQtktR",
);

export default function Home() {
	const [clientSecret, setClientSecret] = useState<string | null>(null);
	const [error, setError] = useState<string | null>(null);

	useEffect(() => {
		let cancelled = false;

		async function startCheckout() {
			try {
				const secret = await paymentsService.createCheckout();
				if (!cancelled) setClientSecret(secret);
			} catch (err) {
				if (!cancelled) {
					setError(
						err instanceof Error ? err.message : "Could not start checkout",
					);
				}
			}
		}

		startCheckout();
		return () => {
			cancelled = true;
		};
	}, []);

	if (error) {
		return <p role="alert">{error}</p>;
	}

	if (!clientSecret) {
		return <p>Loading checkout…</p>;
	}

	return (
		<CheckoutElementsProvider stripe={stripePromise} options={{ clientSecret }}>
			<CheckoutForm />
		</CheckoutElementsProvider>
	);
}
