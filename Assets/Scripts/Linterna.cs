using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;


public class Linterna : MonoBehaviour
{
    public GameObject ON;
    public GameObject OFF;
    private bool isON;

    [SerializeField] float maxPower;
    [SerializeField] float currentPower;
    [SerializeField] Slider batterySliderUI;

    XRIDefaultInputActions input;

    [SerializeField] Transform rayOrigin;

    bool recharging;

    private void Awake()
    {
        input = new XRIDefaultInputActions();

        input.XRIRightHandInteraction.ActivateValue.performed += TorchOn;
        input.XRIRightHandInteraction.ActivateValue.canceled += TorchOn;

    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }


    // Start is called before the first frame update
    void Start()
    {
        ON.SetActive(false);
        OFF.SetActive(true);
        batterySliderUI.gameObject.SetActive(false);
        isON = false;
    }


    private void Update()
    {
        // Update the HMD UI battery
        batterySliderUI.value = currentPower;

        if(!isON && recharging)
        {
            input.Enable();        
        }
        if (isON && currentPower > 0)
        {
            currentPower = Mathf.Clamp(currentPower-Time.deltaTime, 0, maxPower);

            if(currentPower == 0)
            {
                batterySliderUI.gameObject.SetActive(true);
                input.Disable();
                isON = false;
                recharging = true;
                return;
            }

            
            CastRays();
        }
        else
        {
            currentPower = Mathf.Clamp(currentPower + Time.deltaTime, 0, maxPower);

            if(recharging && currentPower == maxPower)
            {
                recharging = false;
                StartCoroutine(ShowBattery());
            } 
        }
    }


    void CastRays()
    {
        RaycastHit hitInfo;

        bool hasHit = Physics.Raycast(rayOrigin.position, rayOrigin.forward, out hitInfo);

        if (hasHit && hitInfo.collider.gameObject.CompareTag("Entity")){
            EntityMovement.instance.collider.enabled = false;
            EntityMovement.instance.EntityFound();
        }
    }

    void TorchOn(InputAction.CallbackContext ctx)
    {
        if (recharging) return;

        var val = ctx.ReadValue<float>();

        //print(val);

        if(val > 0.5f && !isON)
        {
            ON.SetActive(true);
            OFF.SetActive(false);
            isON = true;
        }
        else if(val <= 0.5f && isON)
        {
            ON.SetActive(false);
            OFF.SetActive(true);
            isON= false;
        }
    }

    IEnumerator ShowBattery()
    {
        yield return new WaitForSeconds(3);
        batterySliderUI.gameObject.SetActive(false);
    }

    
}
