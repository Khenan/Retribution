using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractible
{
    public List<Renderer> ObjectRendererToShine { get; }
    public float Cooldown { get; set; }
    public bool Shinning { get; set; }
    public int IdShinning { get; set; }

    public void Interact();

    public void Shine();

    public IEnumerator CooldownCoroutine();
}
