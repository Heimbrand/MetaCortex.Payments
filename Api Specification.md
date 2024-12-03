# Payment API Documentation

## 1. Create a Payment
**Endpoint**: `POST /api/payments`

**Description**: Creates a new payment.

### Request Body:
**Content-Type**: `application/json`

**Example**:
```json
{
    "orderId": "12345",
    "paymentMethod": "credit card",
    "status": "pending"
}
```
---
Response:
Status Code: 201 Created
Location: /api/payments/{paymentId}

Response Body Example:

```json
{
    "paymentId": "12345",
    "orderId": "12345",
    "paymentMethod": "credit card",
    "status": "pending"
}
```
---
Error Response:
Status Code: 400 Bad Request
Response Body:
```json
{
    "message": "Payment data cannot be null"
}
```
---
### 2. Get Payment by ID
Endpoint: GET /api/payments/{paymentId}

Description: Retrieves a payment by its ID.

Request Parameters:
paymentId: The unique identifier of the payment to retrieve.
Response:
Status Code: 200 OK
Response Body Example:
```json
{
    "id": "64d2f6ae3f15c9a2e47a9f01",
    "orderId": "12345",
    "paymentMethod": "credit card",
    "status": "pending"
}
```
Error Response:
Status Code: 404 Not Found
Response Body:
```json
{
    "message": "Payment not found"
}
```
---
### 3. Get All Payments
Endpoint: GET /api/payments

Description: Retrieves a list of all payments.

Response:
Status Code: 200 OK
Response Body Example:
```json
[
    {
        "id": "64d2f6ae3f15c9a2e47a9f01",
        "orderId": "12345",
        "paymentMethod": "credit card",
        "status": "pending"
    },
    {
        "id": "64d2f6ae3f15c9a2e47a9f02",
        "orderId": "12346",
        "paymentMethod": "paypal",
        "status": "completed"
    }
]
```
---
4. Delete a Payment
Endpoint: DELETE /api/payments/{paymentId}

Description: Deletes a payment by its ID.

Request Parameters:
paymentId: The unique identifier of the payment to delete.
Response:
Status Code: 204 No Content
Response Body: Empty (No content).

Error Response:
Status Code: 404 Not Found
Response Body:
```json
{
    "message": "Payment not found"
}
```
---
5. Update a Payment
Endpoint: PUT /api/payments/{paymentId}

Description: Updates an existing payment by its ID.

Request Parameters:
paymentId: The unique identifier of the payment to update.
Request Body:
Content-Type: application/json

Example:
```json
{
    "orderId": "12345",
    "paymentMethod": "credit card",
    "status": "completed"
}
```
---
Response:
Status Code: 200 OK
Response Body Example:
```json
{
    "id": "64d2f6ae3f15c9a2e47a9f01",
    "orderId": "12345",
    "paymentMethod": "credit card",
    "status": "completed"
}
```
Error Response:
400 Bad Request: If the payment body is null or invalid.
Response Body Example:
```json
{
    "message": "Payment data cannot be null"
}
```
404 Not Found: If the payment with the specified paymentId is not found.
Response Body Example:
```json
{
    "message": "Payment not found"
}
```
---
## Summary  
- `POST /api/payments` - Creates a new payment.  
- `GET /api/payments/{paymentId}` - Retrieves a payment by ID.  
- `GET /api/payments` - Retrieves all payments.  
- `DELETE /api/payments/{paymentId}` - Deletes a payment by ID.  
- `PUT /api/payments/{paymentId}` - Updates an existing payment by ID.


