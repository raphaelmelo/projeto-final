import Head from "next/head";
import styles from "@/styles/Home.module.css";
 import { useEffect, useState } from "react";
import axios from "axios";
import Image from 'next/image';

interface Props {
  id?: number;
  titulo: string;
  descricao: string;
}

const CIRCLE = (
  <svg
    width="6"
    height="6"
    viewBox="0 0 6 6"
    fill="none"
    xmlns="http://www.w3.org/2000/svg"
  >
    <path
      fill-rule="evenodd"
      clip-rule="evenodd"
      d="M0 3C0 1.34314 1.34314 0 3 0C4.65684 0 6 1.34314 6 3C6 4.65684 4.65684 6 3 6C1.34314 6 0 4.65684 0 3Z"
      fill="#4F5B56"
    />
  </svg>
);

const RECTANGLE = (
  <svg
    width="4"
    height="15"
    viewBox="0 0 4 15"
    fill="none"
    xmlns="http://www.w3.org/2000/svg"
  >
    <rect width="4" height="15" fill="#4F5B56" />
  </svg>
);

export default function Home() {
  const [list, setList] = useState<Props[]>([]);
  const [todo, setTodo] = useState("");
  const [todoDescription, setTodoDescription] = useState("");
  const [requestNewList, setRequestNewList] = useState(true);
  const [todoId, setTodoId] = useState<number>(0);
  const [showAdd, setShowAdd] = useState(true);
  const [showEdit, setShowEdit] = useState(false);
  axios.defaults.headers.common["Content-Type"] = "application/json";

  useEffect(() => {
    axios
      .get("https://to-do-list-api-qky5.onrender.com/api/tarefas")
      .then((res) => {
        setList(res.data);
        setRequestNewList(false);
      });
  }, [requestNewList]);

  const handleNewTodoSubmit = async (event: React.FormEvent) => {
    event.preventDefault();

    const newTodo = {
      titulo: todo,
      descricao: todoDescription,
    };

    await axios.post<Props>(
      "https://to-do-list-api-qky5.onrender.com/api/tarefas",
      newTodo 
    ).then((res) => {
      if(res.status === 200){
        setRequestNewList(true);
        setShowAdd(true)
        setTodo('')
        setTodoDescription('')
        setTodoId(0)
      }
    })
    .catch((err) => {
      console.log(err)
    })
 

  };

  const handleEdit = async (event: React.FormEvent) => {
    event.preventDefault();

    const editTodo = {
      id:todoId, 
      titulo: todo,
      descricao: todoDescription,
    };

    await axios.put<Props>(
      "https://to-do-list-api-qky5.onrender.com/api/tarefas",
      editTodo 
    );

    setRequestNewList(true);
    setShowEdit(false)
    setShowAdd(true)
    setTodo('')
    setTodoDescription('')
    setTodoId(0)
  };

  const handleDelete = () => {
    console.log("deletando" , todoId)

    if(todoId && todoId != 0){
      console.log(todoId)

      axios.delete(
        `https://to-do-list-api-qky5.onrender.com/api/tarefas/${todoId}`), 500;  
  
        setRequestNewList(true);
        setShowEdit(false)
        setShowAdd(true)
        setTodo('')
        setTodoDescription('')
        setTodoId(0)
    };
    }


  return (
    <>
      <Head>
        <title>Create Next App</title>
      </Head>
      <main className={styles.main}>
        <div className={styles.title}>
          <h1 className={styles.text}>
            lista de
            <br /> <span className={styles.span1}>Tarefas</span>
          </h1>
        </div>
          {showAdd && <div>
          <form onSubmit={handleNewTodoSubmit}>
            <input
              type="titulo"
              value={todo}
              onChange={(event) => setTodo(event.target.value)}
              required
            />
            <input
              type="descricao"
              value={todoDescription}
              onChange={(event) => setTodoDescription(event.target.value)}
              required

            />
            <button type="submit">Novo</button>
          </form>
        </div>}
          {showEdit &&  <div>
          <form onSubmit={handleEdit}>
            <input
              type="titulo"
              value={todo}
              onChange={(event) => setTodo(event.target.value)}
            />
            <input
              type="descricao"
              value={todoDescription}
              onChange={(event) => setTodoDescription(event.target.value)}
            />
            <button type="submit">Editar</button>
          </form>
        </div>}
        <div className={styles.list}>
          {list?.map((item) => {
            return (
              <div className={styles.CardWrapper} key={item.id}>
                <div className={styles.item}>
                  <div>
                    <div className={styles.line}>
                      <div className={styles.lineimg}>
                      <div className={styles.line2img}><Image src="/rectangle.svg" className={styles.rectangle} alt="Ícone" width={6} height={12} /> </div>
                      </div>
                      {item.titulo}
                    </div>
                    <div className={styles.line2}>
                       {item.descricao}
                    </div>
                  </div>
                  <div className={styles.actions}>
                  <div>
                      <button onClick={() => {
                        setTodoId(item.id || 0);

                        if(todoId != 0){
                          handleDelete();
                        }                        
                      }}>
                        <Image src="/delete.svg" alt="Ícone" width={16} height={16} title="Deletar Item"/>
                      </button>
                    </div>
                    <div >
                      <button onClick={() => {
                        setShowAdd(false)
                        setShowEdit(true)                 
                        setTodoId(item.id || 0)
                        setTodo(item.titulo)
                        setTodoDescription(item.descricao)
                      }}>
                        <Image src="/edit.svg" alt="Ícone" width={16} height={16} title="Editar Item"/>

                      </button>
                    </div>

                  </div>
                </div>
              </div>
            );
          }).reverse()}
        </div>
      </main>
    </>
  );
}
