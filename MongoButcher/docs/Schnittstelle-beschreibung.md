# Schnittstelle beschreibung

## Modules

### Product
**Path:** http://{serverUrl}/api/products

| HTTP Verb | URL | Description | Error |
|-|-|-|-|
| GET | /all | <200> List of products,<204> No products |  |
| GET | /{id} | <200> Representation of a product in JSON | <404> (Not Found), if ID not found or invalid. |
| POST | / | <201> Created, 'Location' header with link to /products/{id} containing new ID. | <409> (Conflict) if resource attributes are invalid or the resource already exists. |
| PUT | / | <204> Updated, 'Location' header with link to /product/{id} containing the ID. | <404> (Not Found), if ID not found or invalid. |
| DELETE | /{id} | <204> No content | <404> (Not Found), if ID not found or invalid. |

### Recipe

**Path:** http://{serverUrl}/api/recipes

| HTTP Verb | URL   | Description                                                  | Error                                                        |
| --------- | ----- | ------------------------------------------------------------ | ------------------------------------------------------------ |
| GET       | /all  | <200> List of recipes,<204> No recipes                       |                                                              |
| GET       | /{id} | <200> Representation of a recipe in JSON                     | <404> (Not Found), if ID not found or invalid.               |
| POST      | /     | <201> Created, 'Location' header with link to /recipes/{id} containing new ID. | <409> (Conflict) if resource attributes are invalid or the resource already exists. |
| PUT       | /     | <204> Updated, 'Location' header with link to /recipes/{id} containing the ID. | <404> (Not Found), if ID not found or invalid.               |
| DELETE    | /{id} | <204> No content                                             | <404> (Not Found), if ID not found or invalid.               |

### Production

**Path:** http://{serverUrl}/api/production

| HTTP Verb | URL     | Description                                                  | Error                                                        |
| --------- | ------- | ------------------------------------------------------------ | ------------------------------------------------------------ |
| POST      | /create | <201> Create a specific product specified in the request body with an recipe. 'Location' header with link to /products/{id} containing the ID of the product. | <404> (Not Found), if ID of the prodcts not found or invalid. |

