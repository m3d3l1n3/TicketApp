import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { PRIORITY_LABELS, type Ticket, type Project } from "../types";

export function TicketForm() {
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [priority, setPriority] = useState(0);
  const [dueDate, setDueDate] = useState("");
  const [projectId, setProjectId] = useState<number | null>(null);
  const [projects, setProjects] = useState<Project[]>([]);
  const [error, setError] = useState<string | null>(null);
  const [submitting, setSubmitting] = useState(false);
  const navigate = useNavigate();

  useEffect(() => {
    async function load() {
      try {
        const res = await fetch(`http://localhost:5069/api/projects`);
        if (!res.ok) throw new Error(`API responded ${res.status}`);
        else {
          setProjects(await res.json());
        }
      } catch (e) {
        setError(e instanceof Error ? e.message : "Unknwon error");
      }
    }
    load();
  }, []);

  async function handleSubmit(e: React.SubmitEvent) {
    e.preventDefault();
    setSubmitting(true);
    setError(null);
    try {
      const res = await fetch("http://localhost:5069/api/tickets", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          title,
          description,
          priority,
          projectId,
          dueDate: dueDate === "" ? null : dueDate,
        }),
      });
      if (res.status === 201) {
        const created = (await res.json()) as Ticket;
        navigate(`tickets/${created.id}`);
      } else if (res.status === 400) {
        setError(await res.text());
      } else throw new Error(`API responded ${res.status}`);
    } catch (err) {
      setError(err instanceof Error ? err.message : "error");
    } finally {
      setSubmitting(false);
    }
  }

  return (
    <form onSubmit={handleSubmit}>
      <h1>New ticket</h1>
      {error && <p>{error}</p>}
      <label>
        {" "}
        Title{" "}
        <input value={title} onChange={(e) => setTitle(e.target.value)} />{" "}
      </label>
      <label>
        Description{" "}
        <textarea
          value={description}
          onChange={(e) => setDescription(e.target.value)}
        />
      </label>
      <label>
        Priotity
        <select
          value={priority}
          onChange={(e) => setPriority(Number(e.target.value))}
        >
          {Object.entries(PRIORITY_LABELS).map(([value, label]) => (
            <option key={value} value={value}>
              {label}
            </option>
          ))}
        </select>
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
          value={projectId ?? ""}
          onChange={(e) => setProjectId(Number(e.target.value))}
        >
          <option value="" disabled>
            Select a project.
          </option>
          {projects.map((p) => (
            <option key={p.id} value={p.id}>
              {p.name}
            </option>
          ))}
        </select>
      </label>
      <button type="submit" disabled={submitting || projectId === null}>
        {submitting ? "Creating.." : "Create"}
      </button>
    </form>
  );
}
