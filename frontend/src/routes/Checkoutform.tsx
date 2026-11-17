import {
	ContactDetailsElement,
	PaymentElement,
	useCheckoutElements,
} from "@stripe/react-stripe-js/checkout";
import { type FormEvent, useState } from "react";

const CheckoutForm = () => {
	const checkoutState = useCheckoutElements();
	const [submitting, setSubmitting] = useState(false);
	const [error, setError] = useState<string | null>(null);

	if (checkoutState.type === "loading") {
		return <div>Loading...</div>;
	}

	if (checkoutState.type === "error") {
		return <div>Error: {checkoutState.error.message}</div>;
	}

	const { checkout } = checkoutState;

	const handleSubmit = async (event: FormEvent<HTMLFormElement>) => {
		event.preventDefault();
		setSubmitting(true);
		setError(null);

		const result = await checkout.confirm({ redirect: "if_required" });

		setSubmitting(false);
		if (result.type === "error") {
			setError(result.error.message);
		}
	};

	return (
		<form onSubmit={handleSubmit}>
			<ContactDetailsElement />
			<PaymentElement />
			<p>Total: {checkout.total.total.amount}</p>
			{error && <p role="alert">{error}</p>}
			<button type="submit" disabled={submitting}>
				{submitting ? "Processing…" : "Pay"}
			</button>
		</form>
	);
};

export default CheckoutForm;
