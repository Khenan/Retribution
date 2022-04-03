using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractible
{
    public List<Renderer> ObjectRendererToShine { get; }
    public bool Shining { get; set; }
    public bool Takable { get; set; }

    public void Interact();

    public void Shine();

    public IEnumerator CooldownCoroutine();
}
