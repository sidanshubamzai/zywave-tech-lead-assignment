import { useEffect, useState } from "react";
import { getIncidents, updateIncidentStatus } from "../api/incidentApi";

export default function IncidentList() {
  const [incidents, setIncidents] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");

  const loadIncidents = () => {
    setLoading(true);
    getIncidents()
      .then((data) => setIncidents(data))
      .catch(() => setError("Failed to load incidents"))
      .finally(() => setLoading(false));
  };
    const severityMap = {
        1: "Low",
        2: "Medium",
        3: "High",
    };
  const handleStatusChange = async (id, status) => {
    await updateIncidentStatus(id, status);
    loadIncidents();
  };

  useEffect(() => {
    loadIncidents();
  }, []);

  if (loading) return <p>Loading incidents...</p>;
  if (error) return <p>{error}</p>;
  if (!incidents || incidents.length === 0)
    return <p>No incidents found</p>;

  return (
      <table
          border="1"
          cellPadding="8"
          style={{ borderCollapse: "collapse", marginTop: "16px" }}
      >
      <thead>
        <tr>
          <th>Title</th>
          <th>Severity</th>
          <th>Status</th>
          <th>Attachments</th>
          <th>Created</th>
        </tr>
      </thead>
      <tbody>
        {incidents.map((i) => (
          <tr key={i.id}>
            <td>{i.title}</td>

            <td>{severityMap[i.severity] || "Unknown"}</td>

            <td>
              <select
                value={i.status}
                onChange={(e) =>
                  handleStatusChange(i.id, Number(e.target.value))
                }
              >
                <option value={1}>Open</option>
                <option value={2}>In Progress</option>
                <option value={3}>Resolved</option>
              </select>
            </td>

            <td>
              {i.attachments && i.attachments.length > 0 ? (
                i.attachments.map((a) => (
                  <div key={a.blobUrl}>
                    <a
                      href={a.blobUrl}
                      target="_blank"
                      rel="noreferrer"
                    >
                      {a.fileName}
                    </a>
                  </div>
                ))
              ) : (
                <span>-</span>
              )}
            </td>

            <td>{new Date(i.createdAtUtc).toLocaleString()}</td>
          </tr>
        ))}
      </tbody>
    </table>
  );
}
