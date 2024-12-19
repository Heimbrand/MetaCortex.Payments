```csharp
public class Payment : BaseDocument
{
    public string OrderId { get; set; }
    public string PaymentMethod { get; set; }
    public string Status { get; set; }
}

# Payment API Documentation

## 1. Create a Payment

### Description
Creates a new payment.

### Endpoint
`POST /api/payments`

### Request Body
```json
{
    "orderId": "12345",
    "paymentMethod": "credit card",
    "status": "pending"
}
```

### Response
- **201 Created**
  ```json
  {
      "paymentId": "64d2f6ae3f15c9a2e47a9f01",
      "orderId": "12345",
      "paymentMethod": "credit card",
      "status": "pending"
  }
  ```
- **400 Bad Request**
  ```json
  {
      "message": "Payment data cannot be null"
  }
  ```

## 2. Get Payment by ID

### Description
Retrieves a payment by its ID.

### Endpoint
`GET /api/payments/{paymentId}`

### Response
- **200 OK**
  ```json
  {
      "id": "64d2f6ae3f15c9a2e47a9f01",
      "orderId": "12345",
      "paymentMethod": "credit card",
      "status": "pending"
  }
  ```
- **404 Not Found**
  ```json
  {
      "message": "Payment not found"
  }
  ```

## 3. Get All Payments

### Description
Retrieves a list of all payments.

### Endpoint
`GET /api/payments`

### Response
- **200 OK**
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

## 4. Delete a Payment

### Description
Deletes a payment by its ID.

### Endpoint
`DELETE /api/payments/{paymentId}`

### Response
- **204 No Content** (Empty response body)
- **404 Not Found**
  ```json
  {
      "message": "Payment not found"
  }
  ```

## 5. Update a Payment

### Description
Updates an existing payment by its ID.

### Endpoint
`PUT /api/payments/{paymentId}`

### Request Body
```json
{
    "orderId": "12345",
    "paymentMethod": "credit card",
    "status": "completed"
}
```

### Response
- **200 OK**
  ```json
  {
      "id": "64d2f6ae3f15c9a2e47a9f01",
      "orderId": "12345",
      "paymentMethod": "credit card",
      "status": "completed"
  }
  ```
- **400 Bad Request**
  ```json
  {
      "message": "Payment data cannot be null"
  }
  ```
- **404 Not Found**
  ```json
  {
      "message": "Payment not found"
  }
  ```

## Supporting Request Models

### PaymentCreateRequest
```csharp
public class PaymentCreateRequest
{
    public string OrderId { get; set; }
    public string PaymentMethod { get; set; }
    public string Status { get; set; }
}
```

### PaymentUpdateRequest
```csharp
public class PaymentUpdateRequest
{
    public string PaymentMethod { get; set; }
    public string Status { get; set; }
}
```
```
