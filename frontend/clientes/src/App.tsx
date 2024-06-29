import { BrowserRouter } from "react-router-dom";
import { AppRoutes } from "./routes";
import { ThemeContext } from "./shared/contexts/ThemeContext";

export const App = () => {
  return (
    <ThemeContext>
      <BrowserRouter>
        <AppRoutes />
      </BrowserRouter>
    </ThemeContext>
  );
}