import moment from "moment";

export const getDeliveryTime = (order) => {
  return moment(order.createdAt).add(order.deliveryTime, "hours");
};
