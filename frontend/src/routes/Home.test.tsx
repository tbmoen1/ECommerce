import { render } from "@testing-library/react";
import { vi } from "vitest";
import Home from "./Home";

vi.mock("@stripe/stripe-js", () => ({
	loadStripe: () => Promise.resolve(null),
}));

vi.stubGlobal(
	"fetch",
	vi.fn(() => new Promise(() => {})),
);

test("shows a loading state while the payment intent is created", () => {
	const result = render(<Home />);

	const element = result.getByText(/Loading checkout/i);

	expect(element).toBeInTheDocument();
});
