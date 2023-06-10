import React from "react";
import Card from "react-bootstrap/Card";
import ListGroup from "react-bootstrap/ListGroup";
import OrderItem from "./OrderItem";

const OrderList = ({ orders }) => {
  return (
    <Card>
      <ListGroup variant="flush">
        {orders.map((order) => (
          <ListGroup.Item key={order.id}>
            <OrderItem orderData={order} />
          </ListGroup.Item>
        ))}
      </ListGroup>
    </Card>
  );
};

export default OrderList;
