import { BrowserRouter, Route, Routes } from "react-router";
import ComponentsPage from "./pages/components-page";
import LayoutMain from "./pages/layout-main";
import HomePage from "./pages/home-page";

export default function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route element={<LayoutMain />}>
          <Route index element={<HomePage />} />
          <Route path="/componentes" element={<ComponentsPage />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}
