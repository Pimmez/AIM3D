using UnityEngine;

public class LightCullingMaskControl : LightControl {

    [SerializeField]
    private Camera player1Camera;

    [SerializeField]
    private LayerMask lightOffView;

    [SerializeField]
    private LayerMask lightOnView;

    protected override void SwitchLight(bool _lightState)
    {
        base.SwitchLight(_lightState);

        //the lightOnView hides illusions and shows things that previously only player 2 could see
        if (_lightState) player1Camera.cullingMask = lightOnView.value;

        //the lightOffView shows illusions and hides things player 2 can see
        else player1Camera.cullingMask = lightOffView.value;
    }
}
