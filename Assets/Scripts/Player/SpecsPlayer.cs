using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecsPlayer : MonoBehaviour
{
    public float playerLifePoints;      // Life Points of the player
    private Animator playerAnimator;    // Animator of the player

    public Sprite spriteHeart0;
    public Sprite spriteHeart1;
    public Sprite spriteHeart2;
    public Sprite spriteHeart3;
    public Sprite spriteHeart4;

    private GameObject heartUI_1;
    private GameObject heartUI_2;
    private GameObject heartUI_3;

    void Start()
    {
        playerLifePoints = 2f;
        playerAnimator = GetComponent<Animator>();

        heartUI_1 = GameObject.Find("Heart1");
        heartUI_2 = GameObject.Find("Heart2");
        heartUI_3 = GameObject.Find("Heart3");
    }

    void Update()
    {
        if (playerLifePoints <= 0f)
        {
            gameObject.GetComponent<MovePlayer>().bCanMove = false;
            playerAnimator.SetBool("IsDead", true);
        }

        HealthbarManagement();
    }

    public void KillPlayer()
    {
        Destroy(gameObject);
    }

    public void PlayDeathCry()
    {
        FindObjectOfType<AudioManager>().Play("PlayerDeath");
    }

    private void HealthbarManagement()
    {
        Image imgHeartUI_1 = heartUI_1.GetComponent<Image>();
        Image imgHeartUI_2 = heartUI_2.GetComponent<Image>();
        Image imgHeartUI_3 = heartUI_3.GetComponent<Image>();

        /* Manage heart 1 */
        if (playerLifePoints <= 4f)
        {
            switch (playerLifePoints)
            {
                case 0:
                    imgHeartUI_1.sprite = spriteHeart0;
                    break;
                case 1:
                    imgHeartUI_1.sprite = spriteHeart1;
                    break;
                case 2:
                    imgHeartUI_1.sprite = spriteHeart2;
                    break;
                case 3:
                    imgHeartUI_1.sprite = spriteHeart3;
                    break;
                case 4:
                    imgHeartUI_1.sprite = spriteHeart4;
                    break;
                default:
                    imgHeartUI_1.sprite = spriteHeart0;
                    break;
            }

            imgHeartUI_2.sprite = spriteHeart0;
            imgHeartUI_3.sprite = spriteHeart0;
        }
        /* Manage heart 2 */
        else if (playerLifePoints > 4f && playerLifePoints <= 8f)
        {
            switch (playerLifePoints)
            {
                case 5f:
                    imgHeartUI_2.sprite = spriteHeart1;
                    break;
                case 6f:
                    imgHeartUI_2.sprite = spriteHeart2;
                    break;
                case 7f:
                    imgHeartUI_2.sprite = spriteHeart3;
                    break;
                case 8f:
                    imgHeartUI_2.sprite = spriteHeart4;
                    break;
                default:
                    imgHeartUI_2.sprite = spriteHeart0;
                    break;
            }

            imgHeartUI_1.sprite = spriteHeart4;
            imgHeartUI_3.sprite = spriteHeart0;
        }
        /* Manage heart 3 */
        else if (playerLifePoints > 8f)
        {
            switch (playerLifePoints)
            {
                case 9f:
                    imgHeartUI_3.sprite = spriteHeart1;
                    break;
                case 10f:
                    imgHeartUI_3.sprite = spriteHeart2;
                    break;
                case 11f:
                    imgHeartUI_3.sprite = spriteHeart3;
                    break;
                case 12f:
                    imgHeartUI_3.sprite = spriteHeart4;
                    break;
                default:
                    imgHeartUI_3.sprite = spriteHeart0;
                    break;
            }

            imgHeartUI_1.sprite = spriteHeart4;
            imgHeartUI_2.sprite = spriteHeart4;
        }
    }
}
