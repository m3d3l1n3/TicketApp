import { useState } from "react";
import { useProjects } from "../hooks/useProjects";
import type { Ticket } from "../types";

interface EditFormProps {
  ticket: Ticket;
  onSaved: (updated: Ticket) => void;
  onCancel: () => void;
}

export function EditForm({ ticket, onSaved, onCancel }: EditFormProps) {
  const [title, setTitle] = useState(ticket.title);
  const [description, setDescription] = useState(ticket.description);
  const [dueDate, setDueDate] = useState(ticket.dueDate ?? "");
  const [projectId, setProjectId] = useState(ticket.projectId);
  const [error, setError] = useState<string | null>(null);
  const [submit, setSubmitting] = useState(false);
  const projects = useProjects();

  async function handleSubmit(e: React.SubmitEvent) {
    e.preventDefault();
    setSubmitting(true);
    try {
      const res = await fetch(
        `http://localhost:5069/api/tickets/${ticket?.id}`,
        {
          method: "PUT",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify({
            title,
            description,
            projectId,
            dueDate: dueDate === "" ? null : dueDate,
          }),
        },
      );

      if (res.status === 200) onSaved((await res.json()) as Ticket);
      else if (res.status === 400) setError(await res.text());
      else setError(`API responded ${res.status}`);
    } catch {
      setError("Could not reach API");
    } finally {
      setSubmitting(false);
    }
  }
  return (
    <form onSubmit={handleSubmit}>
      <h1>Edit ticket</h1>
      error && <p>{error}</p>
      <label>
        Title
        <input value={title} onChange={(e) => setTitle(e.target.value)} />
      </label>
      <label>
        Description
        <textarea
          value={description}
          onChange={(e) => setDescription(e.target.value)}
        />
      </label>
      <label>
        Due date
        <input
          type="date"
          value={dueDate}
          onChange={(e) => setDueDate(e.target.value)}
        />
      </label>
      <label>
        Project
        <select
          value={projectId}
          onChange={(e) => setProjectId(Number(e.target.value))}
        >
          {projects.map((p) => (
            <option key={p.id} value={p.id}>
              {p.name}
            </option>
          ))}
        </select>
      </label>
      <button type="submit" disabled={submit}>
        {submit ? "Saving…" : "Save"}
      </button>
      <button type="button" onClick={onCancel} disabled={submit}>
        Cancel
      </button>
    </form>
  );
}
