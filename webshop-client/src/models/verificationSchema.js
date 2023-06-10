import { object, number } from "yup";
import { VerificationStatus } from "./verificationStatus";

export const verificationSchema = object({
  userId: number().required("User is required"),
  verificationStatus: number()
    .required("Verification status is required")
    .oneOf(
      Object.values(VerificationStatus),
      "Verification status is not valid"
    ),
});
