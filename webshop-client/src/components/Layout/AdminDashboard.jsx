import React from "react";
import OrderList from "../Orders/OrderList";
import { useSelector } from "react-redux";

const AdminDashboard = ({ children }) => {
  const { orders } = useSelector((state) => state.orders);

  return (
    <div>
      <h1>Admin Dashboard</h1>
      <hr />
      {children}
      <hr />
      <div>
        <h3>All Orders</h3>
        <OrderList orders={orders} />
      </div>
    </div>
  );
};

export default AdminDashboard;
