import { useEffect, useState } from "react";
import type { Project } from "../types";

export function useProjects(): Project[] {
  const [projects, setProjects] = useState<Project[]>([]);
  useEffect(() => {
    async function load() {
      try {
        const res = await fetch("http://localhost:5069/api/projects");
        if (res.ok) setProjects(await res.json());
      } catch {
        /* dropdown stays empty; the API being down surfaces elsewhere */
      }
    }
    load();
  }, []);
  return projects;
}
