using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractible
{
    public float Cooldown { get; set; }

    public List<Renderer> ObjectRendererToShine { get; }

    public bool Shinning { get; set; }
    public int IdShinning { get; set; }

    public void Interact();

    public void Shine();

    public IEnumerator CooldownCoroutine();
}
