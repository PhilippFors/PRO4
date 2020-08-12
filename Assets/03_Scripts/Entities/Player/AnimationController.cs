using UnityEngine;

using UnityEngine.Playables;

using UnityEngine.Animations;



public class AnimationController : MonoBehaviour
{
    [HideInInspector] public PlayerControls input;
    [HideInInspector] public Vector2 move;
    [HideInInspector] public Vector2 gamepadRotate;

    public AnimationClip forward;
    public AnimationClip backward;
    public AnimationClip right;
    public AnimationClip left;
    public AnimationClip idle;

    

    public float weightX;
    public float weightY;

    PlayableGraph playableGraph;

    AnimationMixerPlayable mixerPlayable;
    
    private void Awake()
    {
        input = new PlayerControls();
    }

    void Start()

    {

        // Creates the graph, the mixer and binds them to the Animator.

        playableGraph = PlayableGraph.Create();

        var playableOutput = AnimationPlayableOutput.Create(playableGraph, "Animation", GetComponentInChildren<Animator>());
        Debug.Log(GetComponentInChildren<Animator>());

        mixerPlayable = AnimationMixerPlayable.Create(playableGraph, 4);

        playableOutput.SetSourcePlayable(mixerPlayable);

        // Creates AnimationClipPlayable and connects them to the mixer.

        var clipPlayable0 = AnimationClipPlayable.Create(playableGraph, forward);

        var clipPlayable1 = AnimationClipPlayable.Create(playableGraph, backward);
        
        var clipPlayable2 = AnimationClipPlayable.Create(playableGraph, left);
        
        var clipPlayable3 = AnimationClipPlayable.Create(playableGraph, right);
        //var clipPlayable4 = AnimationClipPlayable.Create(playableGraph, idle);

        playableGraph.Connect(clipPlayable0, 0, mixerPlayable, 0);

        playableGraph.Connect(clipPlayable1, 0, mixerPlayable, 1);
        playableGraph.Connect(clipPlayable2, 0, mixerPlayable, 2);
        playableGraph.Connect(clipPlayable3, 0, mixerPlayable, 3);
        //playableGraph.Connect(clipPlayable4, 0, mixerPlayable, 4);

        

        // Plays the Graph.

        playableGraph.Play();

    }

    void Update()

    {

        move = input.Gameplay.Movement.ReadValue<Vector2>();
        gamepadRotate = input.Gameplay.Rotate.ReadValue<Vector2>();
        float y = move.y;
        float x = move.x;
        
        float y2 = gamepadRotate.y;
        float x2 = gamepadRotate.x;
        
       // weightY = Mathf.Clamp01(y);
       // weightX = Mathf.Clamp01(x);

        mixerPlayable.SetInputWeight(0, y);
        mixerPlayable.SetInputWeight(1, -y);
        mixerPlayable.SetInputWeight(2, -x);
        mixerPlayable.SetInputWeight(3, x);
        //mixerPlayable.SetInputWeight(4, 1.0f-);

    }

    private void OnEnable()
    {
        input.Enable();
    }
  
    void OnDisable()

    {

        // Destroys all Playables and Outputs created by the graph.
        input.Disable();
        playableGraph.Destroy();

    }
}