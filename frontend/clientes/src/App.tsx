import { BrowserRouter } from "react-router-dom";
import { AppRoutes } from "./routes";
import { ThemeContext } from "./shared/contexts";
import { DrawerContext } from "./shared/contexts";
import { MenuLateral } from "./shared/components";

export const App = () => {
  return (
    <ThemeContext>
      <DrawerContext>
        <BrowserRouter>

        <MenuLateral>
          <AppRoutes />
        </MenuLateral>

        </BrowserRouter>
        </DrawerContext>
    </ThemeContext>
  );
}