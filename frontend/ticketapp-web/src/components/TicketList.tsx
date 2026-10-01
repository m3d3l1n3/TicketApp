import { useEffect, useState } from "react";
import type { Ticket } from "../types";
import { TicketRow } from "./TicketRow";

export function TicketList() {
  // 1. STATE — the data this component is a function of
  const [tickets, setTickets] = useState<Ticket[] | null>(null);
  const [error, setError] = useState<string | null>(null);

 // 2. SIDE WORK — fetch once, after first render
  useEffect(() => {
    async function load() {
      try {
        const res = await fetch("http://localhost:5069/api/tickets");
        if (!res.ok) throw new Error(`API responded ${res.status}`);
        setTickets(await res.json());
      } catch (e) {
        setError(e instanceof Error ? e.message : "Unknown error");
      }
    }
    load();
  }, []);

  // 3. RENDER — three-way conditional
  if (error) return <p>Failed to load: {error}</p>;
  if (tickets === null) return <p>Loading…</p>;
  return (
    <ul>
  {tickets.map((t) => (
    <TicketRow key={t.id} ticket={t} />
  ))}
</ul>
  );
}