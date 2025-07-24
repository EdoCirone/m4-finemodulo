Cartella Pooling

Contiene la logica per il pooling degli oggetti.  
Serve a ottimizzare le performance evitando instanziazione/distruzione continua, soprattutto per elementi ricorrenti (esplosioni, SFX 3D, nemici, ecc).

---

PoolManager.cs:

Sistema singleton che gestisce una serie di pool configurabili via Inspector.  
Per ogni tipo di oggetto crea una coda di oggetti disattivati, che vengono riutilizzati al momento dello spawn.

- Supporta crescita dinamica della pool (se esaurita).
- Richiede che i prefab implementino l’interfaccia `IPooledObject`.

---

IPooledObject.cs:

Interfaccia da implementare per ogni oggetto che verrà gestito dal sistema di pooling.  
Serve per eseguire logiche specifiche quando un oggetto viene spawnato (`OnObjectSpawn`) o assegnare il suo `tag` di appartenenza.
