import { object, string, date, number, ref } from "yup";
import { AllowedRole } from "./userRole";

export const registerSchema = object({
  username: string().required("Username is required"),
  email: string().required("Email is required").email("Email is not valid"),
  password: string().required("Password is required"),
  confirmPassword: string()
    .required("Password confirmation is required")
    .oneOf([ref("password")], "Passwords must match"),
  firstName: string().required("First name if required"),
  lastName: string().required("Last name if required"),
  birthdate: date()
    .required("Birthdate is required")
    .max(new Date(), "Birthdate must be in the past"),
  address: string().required("Address is required"),
  role: number()
    .required("Role is required")
    .oneOf(Object.values(AllowedRole), "Role is not valid"),
});
