Cartella Movement

Contiene la logica per oggetti mobili nella scena: piattaforme, trappole, elementi ambientali.  
Il sistema è pensato per supportare due modalità:

- Movimento continuo (es. oscillazioni sinusoidali)
- Movimento a step (es. loop avanti-indietro)

---

AbstractObjectMovement.cs:

Classe astratta base che definisce il comportamento comune per tutti gli oggetti mobili.  
Gestisce:

- ritardi iniziali
- ciclo `Do → Wait → Reset`
- movimento continuo frame-by-frame

---

RotationObjectMovement.cs:

Estende `AbstractObjectMovement` per gestire rotazioni.  
Supporta:

- rotazioni continue (es. pale, dischi)
- rotazioni sinusoidali avanti-indietro
- rotazioni a step con interpolazione

---

ScaleObjectMovement.cs:

Gestisce oggetti che cambiano scala nel tempo.  
Supporta:

- riduzione/espansione a step
- oscillazione continua con curva sinusoidale

---

TranslationMovement.cs:

Muove oggetti lungo un asse.  
Supporta:

- traslazione avanti/indietro
- attaccamento opzionale del player via `DeltaPosition`, utile per piattaforme mobili
