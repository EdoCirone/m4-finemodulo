# 📁 Scripts

Questa cartella contiene tutti gli script C# del progetto, organizzati in sottocartelle per area funzionale.

Ogni sottocartella rappresenta un dominio logico del gioco (camera, player, danno, ecc).  
L’obiettivo è avere script **modulari**, **chiari** e **facilmente riutilizzabili**.

---

## 📂 Panoramica cartelle

- **Audio/**  
  Gestione di musica e SFX, con supporto per audio spaziale, pooling e sistemi modulari.
  - `MusicScripts/`: musica di sottofondo automatica per scena.
  - `SFXScripts/`: effetti sonori 2D/3D, sistema a nomi, trigger ed eventi.

- **Camera/**  
  Script che controllano il comportamento della telecamera.

- **Collectables/**  
  Gestione dei collezionabili e del loro tracking (interfaccia, eventi, fine livello).

- **Core/**  
  Include classi fondamentali e manager generici.
  - `Pooling/`: object pooling per istanze riutilizzabili.
  - `Spawn/`: checkpoint e respawn logica.
  - `GameLogic/`: timer e fine livello.
  - `SystemUtils/`: caricamento scene, oggetti persistenti.

- **Combat/**  
  Tutto ciò che riguarda danno, cura, proiettili, esplosivi e logiche di combattimento.
  - `Weapons/`: torrette, proiettili, bullet pooling.
  - `Explosive/`: bombe reattive.

- **Movement/**  
  Script che controllano movimenti modulari di piattaforme e oggetti.  
  Supporta modalità continue (sinusoide, loop) o step-based.

- **Player/**  
  Tutti gli script legati al personaggio giocante: movimento, animazioni, vita, respawn e interazione con le piattaforme mobili.

- **UI/**  
  HUD e menu in-game (vite, timer, monete, game over, vittoria, pausa).

- **zz_Edit/**  
  Strumenti editor personalizzati per Unity.  
  Contiene `CreateReadme.cs`, che aggiunge al menu Unity la voce:  
  `Assets → Create → README.txt`  
  Utile perché Unity 2022.3.x non consente di creare `.txt` direttamente dal Project Panel.

---

## ✍️ Naming convention

- **Classi** → `PascalCase` (es: `PlayerController`)
- **File** → stesso nome della classe
- **Campi privati** → `_camelCase`, anche se `[SerializeField]`
- **Campi pubblici** → `PascalCase`
- **Variabili locali** → `camelCase`
- **Parametri funzione** → `camelCase`
- **Proprietà** → `PascalCase`
- **Enum + valori** → `PascalCase`

👉 Convenzioni documentate in `NamingConvention.md` (nella root progetto)

---

## 🛠 Tecnologie e componenti usati

- Unity 2022.3.x LTS
- Sistema singleton e interfacce per flessibilità
- Eventi Unity per interazione tra componenti
- Coroutine, LayerMask, pooling personalizzato
- TextMeshPro per l’interfaccia

---

## 👤 Autore

Questo progetto è stato realizzato come base didattica e showcase tecnico, con attenzione a:

- chiarezza del codice
- architettura modulare
- documentazione leggibile e mantenibile

È pensato per essere estendibile, comprensibile e facile da ereditare.
