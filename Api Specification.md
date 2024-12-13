# Payment API Documentation

## 1. Create a Payment
**Endpoint**: `POST /api/payments`

**Description**: Creates a new payment.

### Request Body:
**Content-Type**: `application/json`


---
Response:
Status Code: 201 Created
Location: /api/payments/{paymentId}

---
Error Response:
Status Code: 400 Bad Request

---
### 2. Get Payment by ID
Endpoint: GET /api/payments/{paymentId}

Description: Retrieves a payment by its ID.

Request Parameters:
paymentId: The unique identifier of the payment to retrieve.
Response:
Status Code: 200 OK

Error Response:
Status Code: 404 Not Found

---
### 3. Get All Payments
Endpoint: GET /api/payments

Description: Retrieves a list of all payments.

Response:
Status Code: 200 OK

Error Response:
Status Code: 404 Not Found

---
### 4. Delete a Payment
Endpoint: DELETE /api/payments/{paymentId}

Description: Deletes a payment by its ID.

Request Parameters:
paymentId: The unique identifier of the payment to delete.
Response:
Status Code: 204 No Content
Response Body: Empty (No content).

Error Response:
Status Code: 404 Not Found


---
### 5. Update a Payment
Endpoint: PUT /api/payments/{paymentId}

Description: Updates an existing payment by its ID.

Request Parameters:
paymentId: The unique identifier of the payment to update.
Request Body:
Content-Type: application/json

---
Response:
Status Code: 200 OK

Error Response:
400 Bad Request: If the payment body is null or invalid.
404 Not Found: If the payment with the specified paymentId is not found.

---
## Summary  
- `POST /api/payments` - Creates a new payment.  
- `GET /api/payments/{paymentId}` - Retrieves a payment by ID.  
- `GET /api/payments` - Retrieves all payments.  
- `DELETE /api/payments/{paymentId}` - Deletes a payment by ID.  
- `PUT /api/payments/{paymentId}` - Updates an existing payment by ID.


