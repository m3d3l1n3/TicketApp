import type { Ticket } from "../types";
import {Link} from "react-router-dom";
interface TicketRowProps {
  ticket: Ticket;
}

export function TicketRow({ ticket }: TicketRowProps) {
  return (
    <li>
      <Link to={`/tickets/${ticket.id}`}>
      <strong>{ticket.title}</strong> — status {ticket.status} — due{" "}
      {ticket.dueDate ?? "no date"}
      </Link>
    </li>
  );
}