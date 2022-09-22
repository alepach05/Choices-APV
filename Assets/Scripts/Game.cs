using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public TextMeshProUGUI enunciado;

    public TextMeshProUGUI[] opciones;

    public Opcion opcionActual;

    public Opcion opcionInicio;

    public GameObject[] btnOpcion;

    public GameObject btnCreditos;

    public GameObject btnRendirse;

    public int contador;

	// Start is called before the first frame update
	void Start()
    {
        cargarBancoOpciones();
        opcionActual = opcionInicio;
        setInicio();
    }

    public void setOpcion() 
    {    
        StartCoroutine(retrasoOpcion());
    }

    public void setInicio()
    {
        enunciado.text = opcionActual.enunciado;
            for (int i = 0; i < opciones.Length; i++)
            {
                opciones[i].text = opcionActual.opciones[i].opcion;
            }
    }

    public void cargarBancoOpciones()
    {
        try
        {
            opcionInicio =
                JsonConvert
                    .DeserializeObject<Opcion>(File
                        .ReadAllText(Application.streamingAssetsPath +
                        "/ChoicesBank.json"));
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
            enunciado.text = ex.Message;
        }
    }

    public void evaluarOpcion(int respuestaJugador)
    {
        opcionActual = opcionActual.opciones[respuestaJugador];
        if(opcionActual.esFinal){
            StartCoroutine(retrasoEscena());
        }else{
            setOpcion();
        }
    }

    IEnumerator retrasoOpcion()
    {
        yield return new WaitForSecondsRealtime(1f);
        setInicio();
        Debug.Log("esperoOpcion");
    }

    IEnumerator retrasoEscena()
    {
        yield return new WaitForSecondsRealtime(1f);
        enunciado.text = opcionActual.enunciado;
                for (int i = 0; i < btnOpcion.Length; i++)
                {
                    btnOpcion[i].SetActive(false);
                }
            btnCreditos.SetActive(true);
            Debug.Log("esperoEscena");
    }
}