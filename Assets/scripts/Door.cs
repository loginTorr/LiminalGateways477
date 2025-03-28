// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using State = DoorState

// public enum DoorState {
//     CLOSED,
//     OPENING,
//     OPEN,
// }

// public class Door : MonoBehaviour{
//     private Dictionary<DoorState, Action> StateMethods;

//     private DoorState CurState { get; private set; }

//     // Start is called before the first frame update
//     void Start() {
//         StateMethods = new {    
//             [DoorState.CLOSED] = StateClosed,
//             [DoorState.OPENING] = StateOpening,
//             [DoorState.OPEN] = StateOpen,
//         };
//         CurState = DoorState.CLOSED;
//     }


//     // Update is called once per frame
//     void Update(){ 
//         if (StateMethods.ContainsKey(CurState)) {
//             StateMethods[CurState]();
//         }
//     }

//     private void StateClosed() {
//     }

//     private void StateOpening() {
//     }

//     private void StateOpen() {
//     }
// }
