1. Beskriv vad lastbalansering innebär (i allmänhet, behöver inte gälla just databaser)
    - Att man har flera instanser av någon resurs (processorkraft, lagring, bandbredd,
      tillgänglighet, etc.), och delar ut arbete till de olika instanserna, medans det för omvärlden
      ser ut som att det bara finns en instans av resursen.
2. Ge ett konkret exempel på där lastbalansering kan vara bra att använda.
    - Man kan dela upp lagringen av data mellan flera diskar, så att de tillsammans agerar som en
      större, snabbare disk (RAID 0).
3. Anta att du bygger en hemsida. Vilka fördelar finns med använda lastbalansering?
   Ange tre fördelar
    - Lastbalansering kan möjliggöra uppdateringar utan downtime.
    - Lastbalansering gör det enkelt att skala upp sidan för fler användare.
    - Lastbalansering kan agera som en sorts backup, som förhindrar downtime vid t.ex. hårdvarufel.
4. Vad är SQL Server Replication?
    - En samling tekniker som synkroniserar innehållet i flera instanser av SQL Server.
5. Vad är Mirroring i SQL Server?
    - Att data lagras i två instanser av SQL Server, varav en är inaktiv, och kan ta över om
      huvudinstansen slutar fungera.
6. Vad är AlwaysOn Availability Groups i SQL Server?
   - Ett nyare alternativ till Mirroring, där det bl.a. kan finnas flera inaktiva instanser.