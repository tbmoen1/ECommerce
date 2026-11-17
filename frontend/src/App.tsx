import { StrictMode } from "react";
import { RouterProvider } from "react-router";
import { ColorModeApplier } from "./components/ColorMode";
import { router } from "./router";

/**
 * Main react entry point for the app.
 */
export default function App() {
	return (
		<StrictMode>
			<ColorModeApplier>
				<RouterProvider router={router} />
			</ColorModeApplier>
		</StrictMode>
	);
}
