Cartella Explosive

Contiene gli script relativi a bombe o esplosivi.

---

Bomb.cs:

Componente che simula una bomba reattiva.  
Quando il player entra nel raggio di attivazione:

- Parte un countdown.
- L’oggetto lampeggia (allarme visivo).
- Alla fine esplode, infliggendo danno tramite `MakeDamage`.

Supporta trigger multipli e disattivazione dopo l'esplosione.
