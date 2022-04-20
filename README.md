# DrinksAPI
| Akce                                                   | Metoda | Endpoint                                 | Vrací (mimo   chyb serveru 5xx)          |
|--------------------------------------------------------|--------|------------------------------------------|------------------------------------------|
| Získat seznam drinků                                   | GET    | /api/drinks                              | Collection<Drink> - Ok                   |
| Získat drink s konkrétním id                           | GET    | /api/drinks/{id}                         | Drink - Ok / NotFound                    |
| Nahradit stávající drink novým                         | PUT    | /api/drinks/{id} + body                  | NoContent, Ok / NotFound / BadRequest    |
| Vytvořit nový drink                                    | POST   | /api/drinks + body                       | Created, Ok                              |
| Smazat drink                                           | DELETE | /api/tags/{id}                           | Ok, NoContent / NotFound                 |
| Získat seznam restaurací                               | GET    | /api/restaurants                         | Collection<Restaurant> - Ok              |
| Získat restauraci s konkrétním id                      | GET    | /api/restaurants/{id}                    | Drink - Ok / NotFound                    |
| Nahradit stávající restauraci novou                    | PUT    | /api/restaurants/{id} + body             | NoContent, Ok / NotFound / BadRequest    |
| Vytvořit novou restauraci                              | POST   | /api/restaurants + body                  | Created, Ok                              |
| Smazat restauraci                                      | DELETE | /api/restaurants/{id}                    | Ok, NoContent / NotFound                 |
| Přidat drink do restaurace                             | POST   | /api/restaurants/{id}/drinks/{id} + body | Drink - Ok / NotFound / BadRequest       |
| Změnit vlastnosti drinku v restauraci                  | PUT    | /api/restaurants/{id}/drinks/{id} + body | Drink - Ok / NotFound / BadRequest       |
| Odstranit drink z restaurace                           | DELETE | /api/restaurants/{id}/drinks/{id}        | Drink - Ok / NotFound                    |
| Získat drinky prodávané v restauraci                   | GET    | /api/restaurants/{id}/drinks             | Collection<Drink> - Ok / NotFound        |
| Získat restaurace ve kterých je drink prodáván         | GET    | /api/drinks/{id}/restaurants             | Collection<Restaurant> - Ok / NotFound   |
| Získat průměrnou cenu určitého drinku                  | GET    | /api/drinks/{id}/restaurants/averagecost | int - Ok / NoContent / NotFound          |
| Získat počet drinků v restauraci                       | GET    | /api/restaurants/{id}/drinks/count       | int - Ok / NotFound                      |
| Získat počet restaurací, ve kterých se drink prodává   | GET    | /api/drinks/{id}/restaurants/count       | int - Ok / NotFound                      |

![dbstructure](/DrinksAPI/Plan/dbstructure.png "dbstructure")
