import React from "react";
import "../../style/Order.css";
import moment from "moment";

const OrderItem = ({ orderData }) => {
  const getDeliveryTime = (order) => {
    const deliveredAt = moment(order.createdAt).add(
      order.deliveryTime,
      "hours"
    );

    return deliveredAt;
  };

  return (
    <div className="order">
      <div className="order-details">
        <h5 className="order-title">
          Order for Article (Id={orderData.articleId})
        </h5>
        <div className="order-delivery">
          {getDeliveryTime(orderData) < moment() ? (
            <>Delivered {getDeliveryTime(orderData).fromNow()}</>
          ) : (
            <>Delivering {getDeliveryTime(orderData).fromNow()}</>
          )}
        </div>
      </div>

      <div className="order-comment">{orderData.comment}</div>

      <div className="order-info">
        <div className="order-quantity">Quantity: {orderData.quantity}</div>
        <div className="order-price">Price: {orderData.price}</div>
        <div className="order-id">ID: {orderData.id}</div>
      </div>
    </div>
  );
};

export default OrderItem;
