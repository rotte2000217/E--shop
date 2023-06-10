import React from "react";
import OrderList from "../Orders/OrderList";
import { useSelector } from "react-redux";

const BuyerDashboard = ({ children }) => {
  const { orders } = useSelector((state) => state.orders);

  const isPreviousOrder = (order) => {
    const createdAt = new Date(order.createdAt);
    const deliveryTime = order.deliveryTime;
    const deliveredAt = new Date();
    deliveredAt.setHours(createdAt.getHours() + deliveryTime);

    return deliveredAt < new Date();
  };

  const previousOrders = orders.filter((order) => isPreviousOrder(order));
  const activeOrders = orders.filter((order) => !isPreviousOrder(order));

  return (
    <div>
      <h1>Buyer Dashboard</h1>
      <hr />
      {children}
      <hr />
      <div>
        <h3>Active Orders</h3>
        {activeOrders && activeOrders.length > 0 ? (
          <OrderList orders={activeOrders} />
        ) : (
          <p>No Active Orders</p>
        )}
      </div>
      <hr />
      <div>
        <h3>Previous Orders</h3>
        {previousOrders && previousOrders.length > 0 ? (
          <OrderList orders={previousOrders} />
        ) : (
          <p>No Previous Orders</p>
        )}
      </div>
    </div>
  );
};

export default BuyerDashboard;
