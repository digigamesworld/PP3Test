// Written by Hamidreza Karamain (AHEngine) - 2017
// Contact Me: ahengine@live.com

using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace AHEngine.UI.Components
{
    public class LoadingIndicator : MonoBehaviour
    {
        [SerializeField] private Image maskImage;

        [SerializeField] private Vector2 amountRange = new(.2f, .8f);
        [SerializeField] private float amountSpeed = 2;
        [SerializeField] private float rotateSpeed = 3;

        private float amountTarget;
        private Transform maskTr;
        private Vector3 rotation;

        private void Awake() =>
            maskTr = maskImage.transform;

        private void OnEnable()
        {
            amountTarget = amountRange.x;
            maskImage.fillAmount = amountRange.y;
        }

        private void Update()
        {
            maskImage.fillAmount = Mathf.MoveTowards(maskImage.fillAmount, amountTarget, amountSpeed * Time.unscaledDeltaTime);

            if (maskImage.fillAmount == amountTarget)
            {
                if (maskImage.fillClockwise = amountTarget == amountRange.x)
                {
                    amountTarget = amountRange.y;
                    rotation.z += 360f * maskImage.fillAmount;
                }
                else
                {
                    amountTarget = amountRange.x;
                    rotation.z -= 360f * maskImage.fillAmount;
                }
            }

            rotation.z -= rotateSpeed * Time.deltaTime;
            maskTr.localEulerAngles = rotation;
        }
    }
}
