import "./App.css";
import {
  Button,
  Container,
  Table,
  Modal,
  ModalHeader,
  ModalDialog,
} from "react-bootstrap";
import { useState, useEffect, use } from "react";
import "bootstrap/dist/css/bootstrap.min.css";

const uri = "http://localhost:5139/";

function updateData(data) {
  const item = {
    id: parseInt(data.id, 10),
    title: data.title,
    content: data.content,
    tagId: data.tagId,
  };
  fetch(uri + `api/note/${data.id}`, {
    method: "PUT",
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json",
    },
    body: JSON.stringify(item),
  }).then(() => console.log(item));
}

function App() {
  const [notes, setNotes] = useState([]);
  const [show, setShow] = useState(false);
  const [editableContent, setEditableContent] = useState("");
  const [editTitle, setEditTitle] = useState(false);
  const [editableTitle, setEditableTitle] = useState(null);
  const [hoveredId, setHoveredId] = useState(null);
  function getData() {
    fetch(uri + "api/note")
      .then((respone) => respone.json())
      .then((data) => setNotes(data));
  }
  function createNote(data) {
    const item = {
      title: data.title,
      content: data.content,
      tagId: data.tagId,
    };
    fetch(uri + "api/note", {
      method: "POST",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify(item),
    }).then(() => getData());
  }
  function deleteData(id) {
    console.log("entrando a deleteData");

    fetch(uri + `api/note/${id}`, {
      method: "DELETE",
    }).then(()=>getData());
  }
  useEffect(() => {
    getData();
  }, []);
  return (
    <>
      <div>
        <Container>
          <div className="mt-5 ms-2">
            <Button onClick={() => setShow(true)}>Create Note</Button>
            <Modal show={show} onHide={() => setShow(false)} centered>
              <Modal.Header closeButton>
                <Modal.Title className="border border-0">
                  <div className="d-flex">
                    {editTitle ? (
                      <input
                        type="text"
                        placeholder="Title"
                        className="form-control d-flex"
                        onChange={(e) => setEditableTitle(e.target.value)}
                      ></input>
                    ) : (
                      editableTitle
                    )}
                    <button
                      className="border border-0 bg-white"
                      onClick={() => {
                        setEditTitle(!editTitle);
                      }}
                    >
                      <i className="fa fa-pen p-1"></i>
                    </button>
                  </div>
                </Modal.Title>
              </Modal.Header>
              <Modal.Body>
                <textarea
                  rows={10}
                  className="form-control"
                  // defaultValue={Note.content}
                  onChange={(e) => setEditableContent(e.target.value)}
                ></textarea>
              </Modal.Body>
              <Modal.Footer>
                <Button
                  variant="secondary"
                  onClick={() => {
                    setShow(false);
                    setEditTitle(false);
                  }}
                >
                  Cerrar
                </Button>
                <Button
                  variant="primary"
                  onClick={() => {
                    const note = {
                      title: editableTitle,
                      content: editableContent,
                    };
                    createNote(note);
                    setShow(false);
                    setEditableTitle('')
                    setEditableContent('')
                    setEditTitle(false)
                  }}
                >
                  Save
                </Button>
              </Modal.Footer>
            </Modal>
          </div>
          <div className="d-flex flex-wrap justify-content-start pt-4">
            {notes.map((note) => (
              <div
                key={note.id}
                className="col-6 col-md-4 col-lg-2 mb-3"
                onMouseEnter={() => setHoveredId(note.id)}
                onMouseLeave={() => setHoveredId(null)}
              >
                <Note
                  Note={note}
                  hovered={hoveredId == note.id}
                  onDeleteClick={() => {
                    deleteData(note.id)
                  }}
                />
              </div>
            ))}
          </div>
        </Container>
      </div>
    </>
  );
}

function Note({ Note, hovered, onDeleteClick }) {
  const [show, setShow] = useState(false);
  const [showDeleteModal, setShowDeleteModal] = useState(false);
  const [editableContent, setEditableContent] = useState("");
  const [editTitle, setEditTitle] = useState(false);
  const [editableTitle, setEditableTitle] = useState(`${Note.title}`);
  return (
    <>
      {!show && (
        <div
          className="shadow border p-3 bg-light m-2"
          style={{ width: "200px", height: "250px", cursor: "pointer" }}
          onClick={() => setShow(true)}
        >
          <h5
            style={{
              height: "25px",
              textOverflow: "ellipsis",
              whiteSpace: "nowrap",
              overflow: "hidden",
            }}
          >
            {Note.title}
          </h5>
          <div>
            <p
              className="text-break overflow-hidden"
              style={{ height: "140px" }}
            >
              {Note.content}
            </p>
            <div className="float-end">
              {hovered && (
                <div>
                  <button
                    className="btn btn-light"
                    onClick={(e) => {
                      e.stopPropagation();
                      setShowDeleteModal(true);
                    }}
                  >
                    <i className="fa fa-trash-can" aria-hidden="true"></i>
                  </button>
                </div>
              )}
            </div>
          </div>
        </div>
      )}

      <Modal show={show} onHide={() => setShow(false)} centered>
        <Modal.Header closeButton>
          <Modal.Title className="border border-0">
            <div className="d-flex">
              {editTitle ? (
                <input
                  type="text"
                  placeholder={Note.title}
                  className="form-control d-flex"
                  onChange={(e) => setEditableTitle(e.target.value)}
                ></input>
              ) : (
                editableTitle
              )}
              <button
                className="border border-0 bg-white"
                onClick={() => {
                  setEditTitle(!editTitle);
                }}
              >
                <i className="fa fa-pen p-1"></i>
              </button>
            </div>
          </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <textarea
            rows={10}
            className="form-control"
            defaultValue={Note.content}
            onChange={(e) => setEditableContent(e.target.value)}
          ></textarea>
        </Modal.Body>
        <Modal.Footer>
          <Button
            variant="secondary"
            onClick={() => {
              setShow(false);
              setEditTitle(false);
            }}
          >
            Cerrar
          </Button>
          <Button
            variant="primary"
            onClick={() => {
              Note.content =
                editableContent === "" ? Note.content : editableContent;
              Note.title = editableTitle;
              updateData(Note);
              setShow(false);
            }}
          >
            Save
          </Button>
        </Modal.Footer>
      </Modal>
      <Modal
        show={showDeleteModal}
        onHide={() => setShowDeleteModal(false)}
        centered
      >
        <Modal.Header className="bg-danger" closeButton>
          <Modal.Title>
            <div>
              <h2 className="text-white">Delete Note</h2>
            </div>
          </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <div>
            <div>
              <p>
                <strong>Are you Sure?</strong>
              </p>
              {Note.title != null && (
                <div>
                  <p>
                    You are going to delte: <strong>{Note.title}</strong>
                  </p>
                </div>
              )}
            </div>
            <div className="float-end">
              <Button
                variant="danger"
                className="me-2"
                onClick={() => {
                  onDeleteClick();
                  setShowDeleteModal(false);
                }}
              >
                Delete
              </Button>
              <Button
                variant="secondary"
                onClick={() => setShowDeleteModal(false)}
              >
                Cancel
              </Button>
            </div>
          </div>
        </Modal.Body>
      </Modal>
    </>
  );
}

export default App;
