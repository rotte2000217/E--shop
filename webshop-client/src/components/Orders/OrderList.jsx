import React from "react";
import Card from "react-bootstrap/Card";
import ListGroup from "react-bootstrap/ListGroup";
import OrderItem from "./OrderItem";
import "../../style/List.css";

const OrderList = ({ orders, canDelete, handleDelete }) => {
  return (
    <>
      {orders && orders.length > 0 ? (
        <Card>
          <ListGroup variant="flush">
            {orders.map((order) => (
              <ListGroup.Item key={order.id}>
                <OrderItem
                  orderData={order}
                  canDelete={canDelete}
                  handleDelete={handleDelete}
                />
              </ListGroup.Item>
            ))}
          </ListGroup>
        </Card>
      ) : (
        <p className="empty-list-message">No available orders</p>
      )}
    </>
  );
};

export default OrderList;
