Cartella SFXScripts

Questa sottocartella contiene gli script per la gestione degli effetti sonori, sia per la UI che per il gameplay 3D.  
Il sistema è pensato per essere modulare e facilmente integrabile con altri componenti tramite eventi di Unity.

---

UISFXManager.cs:

Gestisce gli effetti sonori dei Menu e dell'interfaccia.
Mappa una lista serializzata di SFX in un dizionario `nome → AudioClip`, così da poterli riprodurre in qualsiasi momento con una stringa.

---

SFXSpatial.cs:

Singleton che si occupa di riprodurre effetti sonori 3D nello spazio.  
Per funzionare si appoggia al `PoolManager` e utilizza oggetti audio poolati (`PooledAudioSource`) per ottimizzare le performance.

---

SFXTriggerSpatial.cs:

Trigger semplice da attaccare ad oggetti per far partire un suono nello spazio.  
Chiama `SFXSpatial` passando posizione, nome e volume.  
Utile da usare con AnimationEvent o UnityEvent.

---

SFXPlayer.cs:

Componente da mettere su oggetti che devono suonare effetti localizzati (es. nemici, oggetti ambientali, ecc).  
Ha un AudioSource interno o nei figli.  
Mappa i clip come dizionario, li riproduce a richiesta.

---

SFXBridgePlayer.cs:

Serve come ponte per gli eventi Unity.  
Permette di richiamare un suono da `SFXPlayer` direttamente da un `UnityEvent`, anche senza parametri.  
Utile per player, UI, animazioni, ecc.

---

PooledAudioSource.cs:

Componente che suona un clip e si autodisattiva una volta finito.  
Si aspetta di essere gestito da un sistema di pooling (`PoolManager`).  
Implementa `IPooledObject`.

---

Note generali:

- Gli AudioClip vengono assegnati via Inspector.
- Tutti i componenti funzionano anche da soli, ma danno il meglio se usati insieme.
- Gli script sono pensati per essere riutilizzabili in scene diverse.
