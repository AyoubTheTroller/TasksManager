curl -X PUT "http://localhost:5000/attachment/" -H "Content-Type: application/json" -d "{\"FileName\": "example.txt", \"ContentType\": "text/plain", \"Base64EncodedData\": "SGVsbG8gd29ybGQ=", \"TaskId\": 1}"