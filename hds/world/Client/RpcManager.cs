using System;
using hds.shared;

namespace hds{

	public class RpcManager{

        public RpcManager (){

		}
		
        /** 
         * This handles all RPC requests for the client
         */

		public void HandleRpc(int header,ref byte[] rpcData){
			switch(header){

            case (int)RPCRequestHeader.CLIENT_SPAWN_READY:
                    new PlayerHandler().processSpawn();
                    new PlayerHandler().processAttributes();
                break;

            case (int)RPCRequestHeader.CLIENT_CLOSE_COMBAT:
                Output.WriteRpcLog("CLOSE COMBAT REQUEST");
                new TestUnitHandler().testCloseCombat(ref rpcData);
                break;
            case (int)RPCRequestHeader.CLIENT_RANGE_COMBAT:
                Output.WriteRpcLog("RANGE COMBAT REQUEST");
                new TestUnitHandler().testCloseCombat(ref rpcData);
                break;
			case (int)RPCRequestHeader.CLIENT_CHAT:
				new ChatHandler().processChat(ref rpcData);
				break;

            case (int)RPCRequestHeader.CLIENT_OBJECTINTERACTION_DYNAMIC:
                new ObjectInteractionHandler().processObject(ref rpcData);
                Output.writeToLogForConsole("RPCMAIN : Handle OBJECTINTERACTION_DYNAMIC");
                break;
            case (int)RPCRequestHeader.CLIENT_OBJECTINTERACTION_STATIC:
                new ObjectInteractionHandler().processObject(ref rpcData);
                Output.writeToLogForConsole("RPCMAIN : Handle OBJECTINTERACTION_STATIC");
                break;
            case (int)RPCRequestHeader.CLIENT_JUMP:
                Output.writeToLogForConsole("RPCMAIN : Handle JUMP");
                //new TestUnitHandler().processHyperJumpTest(ref rpcData);
                new TestUnitHandler().processHyperJump(ref rpcData);
                break;
            case (int)RPCRequestHeader.CLIENT_TARGET:
                Store.currentClient.messageQueue.addRpcMessage(PacketsUtils.createMessage("TARGET CHANGE", "MODAL", Store.currentClient));
                break;
            case (int)RPCRequestHeader.CLIENT_MISSION_REQUEST:
                new MissionHandler().processMissionList(ref rpcData);
                break;
            case (int)RPCRequestHeader.CLIENT_MISSION_INFO:
                new MissionHandler().processLoadMissionInfo(ref rpcData);
                break;
            case (int)RPCRequestHeader.CLIENT_MISSION_ACCEPT:
                new MissionHandler().processMissionaccept(ref rpcData);
                break;
            case (int)RPCRequestHeader.CLIENT_MISSION_ABORT:
                new MissionHandler().processAbortMission(ref rpcData);
                break;
            case (int)RPCRequestHeader.CLIENT_PARTY_LEAVE:
                break;
            
            // Team
            case (int)RPCRequestHeader.CLIENT_HANDLE_MISSION_INVITE:
                new TeamHandler().processTeamInviteAnswer(ref rpcData);
                break;
            // Abilitys
            case (int)RPCRequestHeader.CLIENT_ABILITY_HANDLER:
                new AbilityHandler().processAbility(ref rpcData);
                break;
            case (int)RPCRequestHeader.CLIENT_CHANGE_CT:
                new PlayerHelper().processUpdateExp();
                break;
           
            case (int)RPCRequestHeader.CLIENT_ABILITY_LOADER:
                new PlayerHelper().processLoadAbility(ref rpcData);
                break;

            case (int)RPCRequestHeader.CLIENT_HARDLINE_EXIT_LA_CONFIRM:
                new TeleportHandler().processHardlineExitConfirm(ref rpcData);
                break;

            case (int)RPCRequestHeader.CLIENT_READY_WORLDCHANGE:
                Output.WriteLine("RPCMAIN : RESET_RPC detect");
                new TeleportHandler().processTeleportReset(ref rpcData);
                break;
            
           case (int)RPCRequestHeader.CLIENT_TELEPORT_HL:
                new TeleportHandler().processHardlineTeleport(ref rpcData);
                break;
           
           case (int)RPCRequestHeader.CLIENT_HARDLINE_STATUS_REQUEST:
                new TeleportHandler().processHardlineStatusRequest(ref rpcData);
                break;
                    

            // Inventory
            case (int)RPCRequestHeader.CLIENT_ITEM_MOVE_SLOT:
                new InventoryHandler().processItemMove(ref rpcData);
                break;
            
            case (int)RPCRequestHeader.CLIENT_ITEM_RECYCLE:
                new InventoryHandler().processItemDelete(ref rpcData);
                break;
            
            // Vendor
            case (int)RPCRequestHeader.CLIENT_VENDOR_BUY:
                new VendorHandler().processBuyItem(ref rpcData);
                break;

            // MarketPlace
            case (int)RPCRequestHeader.CLIENT_MP_OPEN:
                new TestUnitHandler().processMarketTest(ref rpcData);
                break;

            case (int)RPCRequestHeader.CLIENT_MP_LIST_ITEMS:
                new MarketPlaceHandler().processMarketplaceList(ref rpcData);
                break;


            // Command Helper
            case (int)RPCRequestHeader.CLIENT_CMD_WHEREAMI:
                new CommandHandler().processWhereamiCommand(ref rpcData);
                break;

            // Emote and Mood Helpers
            case (int)RPCRequestHeader.CLIENT_CHANGE_MOOD:
                new PlayerHelper().processMood(ref rpcData);
                break;

            case (int)RPCRequestHeader.CLIENT_REGION_LOADED:

                new RegionHandler().processRegionLoaded(ref rpcData);
                //new PlayerInitHelper().processRegionSettings();
                break;

            default:
				//PASS :D
                string message = "RPCMAIN : Unknown Header "+header.ToString() + " \n Content:\n " + StringUtils.bytesToString_NS(rpcData);
                Output.WriteLine(message);
                Output.WriteRpcLog(message);

                break;
				
			}
		}
	}
}

