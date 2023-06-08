export const AllowedRole = {
  Buyer: 0,
  Seller: 1,
};

export const UserRole = {
  Buyer: 0,
  Seller: 1,
  Admin: 2,
};

export function roleIdToName(roleId) {
  return Object.keys(UserRole).find((key) => UserRole[key] === roleId);
}

export function roleNameToId(roleName) {
  return UserRole[roleName];
}
