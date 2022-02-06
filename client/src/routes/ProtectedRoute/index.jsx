import React from "react";
import { Navigate, Outlet } from "react-router-dom";

function ProtectedRoute() {
  const token = localStorage.getItem("Authorization");
  return !!token ? <Outlet /> : <Navigate to="/auth/login" />;
}

export default ProtectedRoute;
