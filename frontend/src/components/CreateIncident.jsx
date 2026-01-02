import { useState } from "react";
import {
  createIncident,
  uploadAttachment
} from "../api/incidentApi";

export default function CreateIncident({ onCreated }) {
  const [form, setForm] = useState({
    title: "",
    description: "",
    severity: 1,
  });

  const [file, setFile] = useState(null);
  const [submitting, setSubmitting] = useState(false);
  const [error, setError] = useState("");

  const handleSubmit = async (e) => {
    e.preventDefault();
    setSubmitting(true);
    setError("");

    try {
      //  Create incident (JSON)
      const incident = await createIncident({
        title: form.title,
        description: form.description,
        severity: form.severity,
      });

      // Upload attachment (optional)
      if (file) {
        const formData = new FormData();
        formData.append("file", file);

        await uploadAttachment(incident.id, formData);
      }

      // reset form
      setForm({ title: "", description: "", severity: 1 });
      setFile(null);
      onCreated();
    } catch (err) {
      console.error(err);
      setError("Failed to create incident");
    } finally {
      setSubmitting(false);
    }
  };

  return (
    <form onSubmit={handleSubmit} style={{ marginBottom: "20px" }}>
      <h3>Create Incident</h3>

      {error && <p style={{ color: "red" }}>{error}</p>}

      <div>
        <input
          placeholder="Title"
          value={form.title}
          required
          onChange={(e) =>
            setForm({ ...form, title: e.target.value })
          }
        />
      </div>

      <div>
        <textarea
          placeholder="Description"
          value={form.description}
          required
          onChange={(e) =>
            setForm({ ...form, description: e.target.value })
          }
        />
      </div>

      <div>
        <select
          value={form.severity}
          onChange={(e) =>
            setForm({ ...form, severity: Number(e.target.value) })
          }
        >
          <option value={1}>Low</option>
          <option value={2}>Medium</option>
          <option value={3}>High</option>
        </select>
      </div>

      <div>
        <input
          type="file"
          onChange={(e) => setFile(e.target.files[0])}
        />
      </div>

      <button type="submit" disabled={submitting}>
        {submitting ? "Creating..." : "Create Incident"}
      </button>
    </form>
  );
}
