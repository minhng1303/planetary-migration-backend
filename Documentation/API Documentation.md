# API Endpoints

## Authentication

- `POST /api/auth/login`
  - Request: { username, password }
  - Response: { token }

## Planets

- `GET /api/planets` - List all accessible planets
- `GET /api/planets/{id}` - Get planet details
- `POST /api/planets` - Create new planet (Admin only)
- `PUT /api/planets/{id}` - Update planet (Admin only)
- `DELETE /api/planets/{id}` - Delete planet (SuperAdmin only)
- `POST /api/planets/{id}/factors` - Add factor to planet
- `GET /api/planets/evaluate` - Run evaluation

## Data Models

```json
{
  "Planet": {
    "id": "number",
    "name": "string",
    "description": "string",
    "factors": "PlanetFactor[]"
  },
  "PlanetFactor": {
    "id": "number",
    "name": "string",
    "value": "string",
    "weight": "number"
  }
}
```
