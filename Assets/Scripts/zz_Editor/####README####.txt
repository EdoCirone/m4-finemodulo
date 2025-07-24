Cartella zz_Edit

Contiene strumenti e script di supporto all’editor Unity.  
È posizionata in fondo all’albero del progetto per restare fuori dalla logica di gameplay.

---

CreateReadme.cs:

Script editor che aggiunge una voce personalizzata al menu di Unity:  
**Assets → Create → README.txt**

Serve per creare file README visibili nel Project, completi di `.meta`, in modo sicuro e veloce.

Abbiamo incluso questo script perché nella versione attuale di Unity (2022.3.x) **non esiste un'opzione nativa per creare file `.txt` dal menu contestuale**, e farlo a mano da fuori Unity può rompere i riferimenti `.meta`.

Alla creazione, il file viene generato vuoto, pronto per essere modificato tramite il tuo editor preferito.
