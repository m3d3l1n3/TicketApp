import {useParams, Link} from "react-router-dom";
import { useState, useEffect } from "react";
import { PRIORITY_LABELS, STATUS_LABELS, type Ticket } from "../types";

export function TicketDetails(){
    const params = useParams<{id : string}>();
    const id = Number(params.id);

     // 1. STATE — the data this component is a function of
      const [ticket, setTicket] = useState<Ticket| null>(null);
      const [notFound, setNotFound] = useState(false);
      const [error, setError] = useState<string | null>(null);
    
     // 2. SIDE WORK — fetch once, after first render
      useEffect(() => {
        async function load() {
          try {
            const res = await fetch(`http://localhost:5069/api/tickets/${id}`);
            if (res.status === 404) setNotFound(true);
            else if(!res.ok) throw new Error(`API responded ${res.status}`);
            else setTicket(await res.json());
          } catch (e) {
            setError(e instanceof Error ? e.message : "Unknown error");
          }
        }
        load();
      }, [id]);
    
      // 3. RENDER — three-way conditional
      if (error) return <p>Failed to load: {error}</p>;
      if(notFound) return <p>Ticket not found. <Link to="/">Back to list</Link></p>
      if (ticket === null) return <p>Loading…</p>;
      return (
        <div>
            <Link to="/">Back to list</Link>
            <h1>{ticket.title}</h1>
            <p>{ticket.description}</p>
            <p>Status: {STATUS_LABELS[ticket.status]}</p>
            <p>Priority: {PRIORITY_LABELS[ticket.priority]}</p>
            <p>Due: {ticket.dueDate ?? "No due date"}</p>
            <p>Created: {new Date(ticket.createdAt).toLocaleString()}</p>
        </div>);
}