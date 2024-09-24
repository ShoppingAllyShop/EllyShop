import "./App.css";
import { Route, Routes } from "react-router-dom";
import AdminLayout from "./admin/layout/AdminLayout";
import Dashboard from "./admin/pages/Dashboard/Dashboard";
import Category from "./admin/pages/Category/CategoryPage";
import LoginPage from "./admin/pages/UserAccount/LoginPage";
import { Fragment } from "react";
import PrivateRoute from "./routes/PrivateRoute";
import { ADMIN_ENDPOINT } from "./constants/endpoint";
import ErrorPage from "./components/ErrorPage";

function App() {
  const renderRoutes = () => {
    return (
      <Fragment>
        <Route path="/admin/login" element={<LoginPage />} />
        <Route path={ADMIN_ENDPOINT.ERROR} element={<ErrorPage />} />
        <Route path="/admin" element={<PrivateRoute endpoint=""><AdminLayout /></PrivateRoute>}>
          <Route index path="/admin/dashboard" element={<PrivateRoute endpoint="/dashboard"><Dashboard /></PrivateRoute>}/>
          <Route index path="/admin/category" element={<PrivateRoute endpoint="/category"><Category /></PrivateRoute>} />
        </Route>
      </Fragment>
    );
  };
  
  return <Routes>{renderRoutes()}</Routes>;
}

export default App;
