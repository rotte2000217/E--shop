export function articleResponseDto(data) {
  return {
    id: data.id,
    name: data.name,
    price: data.price,
    quantity: data.quantity,
    description: data.description,
    sellerId: data.sellerId,
  };
}
