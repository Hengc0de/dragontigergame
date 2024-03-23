using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public List<Card>  deck = new List<Card>();
    Card randCard, randCard2;
    public Transform[] cardSlots;
    public int dragonResult, tigerResult ;
    private int won, dragonWin, tieWin, tigerWin, balance, dragonTempoBetAmount, tieTempoBetAmount, tigerTempoBetAmount, betAmount, tieCalCoin, dragonCalCoin, tigerCalCoin;
    public TextMeshProUGUI youWinText, betAmountText, totalBetAmountText, dragonResultText, tigerResultText, timer , balanceText, tieTempoBetAmountText, dragonTempoBetAmountText, tigerTempoBetAmountText;
    public float timeleft = 10;
    public GameObject Artboard2, Artboard3, dragonBtn, tieBtn, tigerBtn, increaseBetAmountBtn, decreaseBetAmountBtn, clone, clone2, PopUpCanvas, playBtn, mainScreenCanvas;
    public GameObject[] CardDeck, CardDeck2;
    private bool timerStopped, notEnoughMoney, creditChecked, BetReady, startTimer;


    public void DrawCards(){
        //Draw Card For Dragon
            
                      
            StartCoroutine(RotateCard());

            
        //Draw Card For Tiger


        

            
        
    }
    public void PlayButton()
    {
        string choicename = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        if (choicename == "PlayBtn")
        {
            mainScreenCanvas.SetActive(false);
        }
    }
    IEnumerator RotateCard()
    {
        

        BetReady = false;


        //dragonResult = Random.Range(0, deck.Count);
        //randCard = deck[dragonResult];
        //randCard.gameObject.SetActive(true);
        //randCard.transform.position = cardSlots[0].position;
        dragonResult = Random.Range(0, CardDeck.Length);
        clone = Instantiate(CardDeck[dragonResult], cardSlots[0].position, cardSlots[0].rotation);
        dragonResult++;
        
        for (float i = 0f; i <= 180f; i += 10f)
        {
            clone.transform.rotation = Quaternion.Euler(0f, i, 0f);
            if (i == 90f)
            {

                Artboard2.gameObject.SetActive(false);

            }
            yield return new WaitForSeconds(0.01f);


        }


        //tigerResult = Random.Range(0, deck.Count);
        //randCard2 = deck[tigerResult];
        //randCard2.gameObject.SetActive(true);

        //randCard2.transform.position = cardSlots[1].position;
        tigerResult = Random.Range(0, CardDeck2.Length);
        clone2 = Instantiate(CardDeck2[tigerResult], cardSlots[1].position, cardSlots[1].rotation);
        tigerResult++;
        for (float i = 0f; i <= 180f; i += 10f)
        {
            clone2.transform.rotation = Quaternion.Euler(0f, i, 0f);
            if (i == 90f)
            {

                Artboard3.gameObject.SetActive(false);

            }
            yield return new WaitForSeconds(0.01f);


        }
            
        

        if (dragonResult > tigerResult)
        {
            
            dragonCalCoin = dragonTempoBetAmount;

            balance = balance + (dragonCalCoin * 2);
             won = dragonCalCoin * 2;
        }
        else if (dragonResult < tigerResult)
        {
             
            tigerCalCoin = tigerTempoBetAmount;
            balance = balance + (tigerCalCoin * 2);
             won = tigerCalCoin * 2;

        }
        else if (dragonResult == tigerResult)
        {
            
            tieCalCoin = tieTempoBetAmount;
            balance = balance + (tieCalCoin * 10);
             won = tieCalCoin * 4;

        }
        tieTempoBetAmount = 0;
        dragonTempoBetAmount = 0;
        tigerTempoBetAmount = 0;
        yield return new WaitForSeconds(2f);

        StartCoroutine(FlipBackCard());
        yield return new WaitForSeconds(1f);


        timeleft = 10;

        startTimer = true;
    }
    private void Awake()
    {
        
    }

    IEnumerator FlipBackCard()
    {
        if (tieCalCoin != 0  || dragonCalCoin != 0 || tigerCalCoin != 0)
        {
            PopUpCanvas.SetActive(true);
            yield return new WaitForSeconds(3f);
            PopUpCanvas.SetActive(false);
        }


        for (float i = 180f; i >= 0f; i -= 10f)
        {
            clone.transform.rotation = Quaternion.Euler(0f, i, 0f);
            if (i == 90f)
            {

                Artboard2.gameObject.SetActive(true);
                Destroy(clone, 2f);
                clone.SetActive(false);

            }
            yield return new WaitForSeconds(0.01f);


        }
        for (float i = 180f; i >= 0f; i -= 10f)
        {
            clone2.transform.rotation = Quaternion.Euler(0f, i, 0f);
            if (i == 90f)
            {

                Artboard3.gameObject.SetActive(true);
                Destroy(clone2, 2f);

                clone2.SetActive(false);



            }
            yield return new WaitForSeconds(0.01f);


        }


        
    }
    public void ChangeBetAmount()
    {

        string choicename = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;

        switch (choicename)
        {
            case "IncreaseBetAmountBtn":

                betAmount += 100;


                break;
            case "DecreaseBetAmountBtn":
                betAmount -= 100;

                if (betAmount <= 0)
                {
                    betAmount = 100;
                }
                break;

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        timeleft = 10;
        startTimer = true;
        BetReady = false;
        balance = 10000;
        tieTempoBetAmount = 0;
        dragonTempoBetAmount =0;
        tigerTempoBetAmount = 0;
        betAmount = 100;
    }
   

        
    
    // Update is called once per frame
    void Update()
    {
        timer.text = Mathf.Round(timeleft).ToString() + "s";

        if (startTimer == true){
            
            timeleft -= Time.deltaTime;
            if (timeleft < 0)
            {
                timerStopped = true;
                print("time left < 0");
                

            }
        }

        if (timerStopped == true){
            //timeleft = 10;
            //   DrawCards();
            StartCoroutine(RotateCard());
            timerStopped = false;
            startTimer=false;
            
            
            

        }
        youWinText.text = "You won: " + won.ToString();
        betAmountText.text = betAmount.ToString() + "$";

        totalBetAmountText.text ="Total bet:" + (tieTempoBetAmount + tigerTempoBetAmount + dragonTempoBetAmount).ToString() + "$" ;


        balanceText.text = balance.ToString() + "$";
        tieTempoBetAmountText.text = tieTempoBetAmount.ToString() + "$";
        tigerTempoBetAmountText.text = tigerTempoBetAmount.ToString() + "$";
        dragonTempoBetAmountText.text = dragonTempoBetAmount.ToString() + "$";

        

        dragonResultText.text = dragonResult.ToString();
        tigerResultText.text = tigerResult.ToString();
    }
    public void GetChoice(){
        if (startTimer){
            
        
            string choicename = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        
            switch (choicename){
                case "DragonBtn":  
                 CheckCredit();
                 if (creditChecked){
                   dragonTempoBetAmount += betAmount;
                    
                    
                    
                 } 
                print(choicename);

                break;
            case "TieBtn":
                 CheckCredit();
                if (creditChecked){
                     tieTempoBetAmount += betAmount;
                 }
                print(choicename);
                
                break;
            case "TigerBtn":
                 CheckCredit();
                 if (creditChecked){
                     tigerTempoBetAmount += betAmount;
                 }
                print(choicename);
                
                break;
            }
            
        }else {
            print("time out");
        }
    }
     private void CheckCredit(){
         if (betAmount > balance){
             notEnoughMoney = true;
            
             creditChecked = false;

         }else{
            balance -= betAmount;
             creditChecked = true;
            

         }
        
     }
    
}
