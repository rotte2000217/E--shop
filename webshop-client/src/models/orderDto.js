export function orderResponseDto(data) {
  return {
    id: data.id,
    quantity: data.quantity,
    comment: data.comment,
    address: data.address,
    articleId: data.articleId,
    buyerId: data.buyerId,
    createdAt: data.createdAt,
    deliveryTime: data.deliveryTime,
    price: data.price,
  };
}
