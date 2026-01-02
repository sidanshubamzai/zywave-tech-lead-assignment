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
      <h2>Create Incident</h2>

      {error && <p style={{ color: "red" }}>{error}</p>}

      <div style={{ marginBottom: "10px" }}>
        <input
          placeholder="Title"
          value={form.title}
          required
          onChange={(e) =>
            setForm({ ...form, title: e.target.value })
          }
        />
      </div>

      <div style={{ marginBottom: "10px" }}>
        <textarea
          placeholder="Description"
          value={form.description}
          required
          onChange={(e) =>
            setForm({ ...form, description: e.target.value })
          }
        />
      </div>

     <div style={{ marginBottom: "10px" }}>
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

      <div style={{ marginBottom: "10px" }}>
        <input
          type="file"
          onChange={(e) => setFile(e.target.files[0])}
        />
      </div>

          <button type="submit" disabled={submitting} style={{
              padding: "6px 12px",
              cursor: "pointer"
          }}>
              {submitting ? "Creating..." : "Create Incident"}
          </button>

    </form>
  );
}
