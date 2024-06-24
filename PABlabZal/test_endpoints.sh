BASE_URL="http://localhost:7126/api"

echo "GET /api/Cars"
curl -X GET "$BASE_URL/Cars"
echo ""

echo "POST /api/Cars"
CAR_ID=$(curl -s -X POST -H "Content-Type: application/json" -d '{"MadeBy": "Toyota", "Model": "Camry", "LicensePlate": "ABC123", "PricePerDay": 50.00}' "$BASE_URL/Cars" | jq -r '.id')
echo "Created Car ID: $CAR_ID"
echo ""

echo "GET /api/Cars/$CAR_ID"
curl -X GET "$BASE_URL/Cars/$CAR_ID"
echo ""

echo "PUT /api/Cars/$CAR_ID"
curl -X PUT -H "Content-Type: application/json" -d '{"Id": '$CAR_ID', "MadeBy": "Toyota", "Model": "Camry", "LicensePlate": "XYZ789", "PricePerDay": 55.00}' "$BASE_URL/Cars/$CAR_ID"
echo ""

echo "DELETE /api/Cars/$CAR_ID"
curl -X DELETE "$BASE_URL/Cars/$CAR_ID"
echo ""

echo "GET /api/Clients"
curl -X GET "$BASE_URL/Clients"
echo ""

echo "POST /api/Clients"
CLIENT_ID=$(curl -s -X POST -H "Content-Type: application/json" -d '{"Name": "John", "Surname": "Doe", "PhoneNumber": "123456789"}' "$BASE_URL/Clients" | jq -r '.id')
echo "Created Client ID: $CLIENT_ID"
echo ""

echo "GET /api/Clients/$CLIENT_ID"
curl -X GET "$BASE_URL/Clients/$CLIENT_ID"
echo ""

echo "PUT /api/Clients/$CLIENT_ID"
curl -X PUT -H "Content-Type: application/json" -d '{"Id": '$CLIENT_ID', "Name": "Jane", "Surname": "Smith", "PhoneNumber": "987654321"}' "$BASE_URL/Clients/$CLIENT_ID"
echo ""

echo "DELETE /api/Clients/$CLIENT_ID"
curl -X DELETE "$BASE_URL/Clients/$CLIENT_ID"
echo ""

echo "GET /api/Employees"
curl -X GET "$BASE_URL/Employees"
echo ""

echo "POST /api/Employees"
EMPLOYEE_ID=$(curl -s -X POST -H "Content-Type: application/json" -d '{"Name": "Alice", "Position": "Manager"}' "$BASE_URL/Employees" | jq -r '.id')
echo "Created Employee ID: $EMPLOYEE_ID"
echo ""

echo "GET /api/Employees/$EMPLOYEE_ID"
curl -X GET "$BASE_URL/Employees/$EMPLOYEE_ID"
echo ""

echo "PUT /api/Employees/$EMPLOYEE_ID"
curl -X PUT -H "Content-Type: application/json" -d '{"Id": '$EMPLOYEE_ID', "Name": "Bob", "Position": "Supervisor"}' "$BASE_URL/Employees/$EMPLOYEE_ID"
echo ""

echo "DELETE /api/Employees/$EMPLOYEE_ID"
curl -X DELETE "$BASE_URL/Employees/$EMPLOYEE_ID"
echo ""

echo "GET /api/Payments"
curl -X GET "$BASE_URL/Payments"
echo ""

echo "POST /api/Payments"
PAYMENT_ID=$(curl -s -X POST -H "Content-Type: application/json" -d '{"Amount": 100.00, "PaymentDate": "2024-06-23T10:00:00Z", "RentalId": 1}' "$BASE_URL/Payments" | jq -r '.id')
echo "Created Payment ID: $PAYMENT_ID"
echo ""

echo "GET /api/Payments/$PAYMENT_ID"
curl -X GET "$BASE_URL/Payments/$PAYMENT_ID"
echo ""

echo "PUT /api/Payments/$PAYMENT_ID"
curl -X PUT -H "Content-Type: application/json" -d '{"Id": '$PAYMENT_ID', "Amount": 120.00, "PaymentDate": "2024-06-23T11:00:00Z", "RentalId": 1}' "$BASE_URL/Payments/$PAYMENT_ID"
echo ""

echo "DELETE /api/Payments/$PAYMENT_ID"
curl -X DELETE "$BASE_URL/Payments/$PAYMENT_ID"
echo ""

echo "GET /api/Rentals"
curl -X GET "$BASE_URL/Rentals"
echo ""

echo "POST /api/Rentals"
RENTAL_ID=$(curl -s -X POST -H "Content-Type: application/json" -d '{"StartDate": "2024-06-23T12:00:00Z", "EndDate": "2024-06-25T12:00:00Z", "CarId": 1, "ClientId": 1, "EmployeeId": 1}' "$BASE_URL/Rentals" | jq -r '.id')
echo "Created Rental ID: $RENTAL_ID"
echo ""

echo "GET /api/Rentals/$RENTAL_ID"
curl -X GET "$BASE_URL/Rentals/$RENTAL_ID"
echo ""

echo "PUT /api/Rentals/$RENTAL_ID"
curl -X PUT -H "Content-Type: application/json" -d '{"Id": '$RENTAL_ID', "StartDate": "2024-06-23T12:00:00Z", "EndDate": "2024-06-26T12:00:00Z", "CarId": 1, "ClientId": 1, "EmployeeId": 1}' "$BASE_URL/Rentals/$RENTAL_ID"
echo ""

echo "DELETE /api/Rentals/$RENTAL_ID"
curl -X DELETE "$BASE_URL/Rentals/$RENTAL_ID"
echo ""
